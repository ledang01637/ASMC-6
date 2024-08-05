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
    } else if (status === "EmptyPro") {
        Swal.fire({
            title: "Bạn không có sản phẩm nào",
            text: "Vui lòng thêm sản phẩm vào giỏ hàng",
            icon: "warning",
        });
    }
}

function showAddOrder(status) {
    if (status === "AddOrder") {
        Swal.fire({
            title: "Đặt hàng thành công",
            icon: "success",
        });
    }
}

function showRegisterAlert(status) {
    if (status === "InputExits") {
        Swal.fire({
            title: "Đăng ký thất bại",
            text: "Email đã tồn tại",
            icon: "warning",
        });
    } else {
        Swal.fire({
            title: "Đăng ký thành công",
            text: "Đăng ký tài khoản thành công",
            icon: "success",
        });
    } 
}

function checkTokenExpiry() {
    const token = localStorage.getItem('authToken');
    const expiryTime = localStorage.getItem('expiryTime');

    if (!token || !expiryTime) {
        return; 
    }

    const currentTime = new Date().toISOString();
    if (new Date(currentTime) > new Date(expiryTime)) {
        localStorage.removeItem('authToken');
        localStorage.removeItem('userName');
        localStorage.removeItem('expiryTime');
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
