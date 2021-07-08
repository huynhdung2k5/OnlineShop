var mThemMoi = new Modal("themmoi");

$(window).ready(function () {

    mThemMoi.setTitle("Thêm mới Sản Phẩm");
    mThemMoi.setDefaultFooterButton("Lưu", "Hủy");
    mThemMoi.hideModalAfterEndPrimaryEvent = false;
    $("#divThemMoi>div").appendTo(mThemMoi.modalBody);

    $("#btnThemMoi").click(function (ev) {
        mThemMoi.show();
    });
    mThemMoi.setPrimaryButtonEvent(function () {
        var form = $("#fThemMoi");
        $.validator.unobtrusive.parse(form);
        var valid = form.valid();

        if (valid == true) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.attr('action'),
                    data: form.serialize(),
                    success: function (response) {
                        if (response.isValid) {
                            form.trigger('reset');
                            Updatedata(getCurrentPage());
                        }
                        mThemMoi.hide();
                        Toast.success("Thêm thành công");
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }
            catch (e) {
                console.log(e);
            }
        }
    });
});

var Updatedata = (_page = 1)=> {
    $.get("/Product/IndexProc", {page: _page, mode: 1}, function (res) {
        $("#view-all").html(res);
    }), "html"
};

jQueryAjaxPost = (ev) => {
    ev.preventDefault();
    $.validator.unobtrusive.parse('form');
    var valid = $(ev.currentTarget).valid();

    if (valid == true) {
        try {
            $.ajax({
                type: 'POST',
                url: ev.currentTarget.action,
                data: new FormData(ev.currentTarget),
                contentType: false,
                processData: false,
                success: function (respone) {
                    if (respone.isValid) {
                        $("#view-all").html(respone.html);
                        $("#form-modal .modal-body").html('');
                        $("#form-modal .modal-title").html('');
                        $("#form-modal").modal('hide');
                    }
                    else {
                        $("#form-modal .modal-body").html(respone.html);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
        catch (e) {
            console.log(e);
        }
    }

    return false;
}

// cái ổ khóa 
$(document).on("click", ".js-btn-lock", (ev) => {
    ev.preventDefault();
    var target = $(ev.currentTarget);
    $.get(target.attr("href"), {}, (response, status, xhr) => {
        if (response == true) {
            Updatedata(getCurrentPage());
            Toast.success("Cập nhật thành công");
        } else {
            Toast.danger("Cập nhật thất bại");
        }
    }, "json");
});

function getCurrentPage() {
    var currentPage = $(".js-pagination li.active").text();
    return currentPage;
}

$(document).on("click", ".js-pagination li>a", function (ev) {
    ev.preventDefault();
    var url = ev.currentTarget.href;
    $.get(url, {mode : 1},
        function (data, textStatus, jqXHR) {
            $("#view-all").html(data);
        },
        "html"
    );
});

$(document).on("click", "#trash", function (ev) {
    ev.preventDefault();
    var url = ev.currentTarget.href;
    $.get(url, {},
        function (data, textStatus, jqXHR) {
            $("#view-all").html(data);
        },
        "html"
    );
});