function showLoginAlert(status) {
    if (status === "InputNull") {
        Swal.fire({
            title: "Đăng nhập thất bại",
            text: "Vui lòng nhập tài khoản và mật khẩu",
            icon: "error",
        });
    } else if (status === "False") {
        Swal.fire({
            title: "Đăng nhập thất bại",
            text: "Tài khoản hoặc mật khẩu không chính xác",
            icon: "error",
        });
    } else if (status === "Block") {
        Swal.fire({
            title: "Đăng nhập thất bại",
            text: "Tài khoản đã bị khóa",
            icon: "warning",
        });
    } else if (status === "True") {
        Swal.fire({
            title: "Đăng nhập thành công",
            icon: "success",
        });
    } else if (status === "NoRole") {
        Swal.fire({
            title: "Đăng nhập thất bại",
            text: "Tài khoản không có quyền truy cập",
            icon: "error",
        });
    }
}
window.cartFunctions = {
    getCart: function () {
        return sessionStorage.getItem('cart');
    },
    saveCart: function (cart) {
        sessionStorage.setItem('cart', cart);
    }
};
