var mThemMoi = new Modal("themmoi");

$(window).ready(function () {

    mThemMoi.setTitle("Thêm mới Sản Phẩm");
    mThemMoi.useLargeSize();
    mThemMoi.setDefaultFooterButton("Lưu", "Hủy");
    mThemMoi.hideModalAfterEndPrimaryEvent = false;
    $("#divThemMoi>div").appendTo(mThemMoi.modalBody);

    $(document).on("click","#btnThemMoi", function (ev) {
        mThemMoi.show();
    });
    mThemMoi.setPrimaryButtonEvent(function () {
        var form = $("#fThemMoi");
        $.validator.unobtrusive.parse(form);
        var valid = form.valid();

        if (valid == true) {
            try {
                var data = new FormData(form[0]);
                $.ajax({
                    type: 'POST',
                    url: form.attr('action'),
                    data: data,
                    processData: false,
                    contentType: false,
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