function showAlert(status) {
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
    } else if (status == "True") {
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
    } else if (status === "AddOrder") {
        Swal.fire({
            title: "Đặt hàng thành công",
            icon: "success",
        });
    } else if (status === "success") {
        Swal.fire({
            title: "Đăng ký thành công",
            icon: "success",
        });
    } else if (status === "InputPhoneExits") {
        Swal.fire({
            title: "Đăng ký thất bại ",
            text: "Số điện thoại đã tồn tại",
            icon: "error",
        });
    } else if (status === "Error") {
        Swal.fire({
            title: "Đăng ký thất bại ",
            text: "Đã có lỗi xảy ra vui lòng đăng ký lại sau ít phút",
            icon: "error",
        });
    } else if (status === "InputEmailExits") {
        Swal.fire({
            title: "Đăng ký thất bại",
            text: "Email đã tồn tại",
            icon: "warning",
        });
    } else if (status === "UpdateSuccessLogin") {
        Swal.fire({
            title: "Cập nhật thông tin thành công",
            icon: "success",
        });
    } else if (status === "ProductSuccess") {
        Swal.fire({
            title: "Thành công",
            icon: "success",
        });
    } else if (status === "ProductFail") {
        Swal.fire({
            title: "Thất bại",
            text: "Sản phẩm đã tồn tại",
            icon: "error",
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

function showModal(modalId) {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
        var modal = new bootstrap.Modal(modalElement);
        modal.show();
    }
}
function closeModal(modalId) {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
        var modal = new bootstrap.Modal(modalElement);
        modal.hide();
    }
}

function openChatGPT(query) {
    try {
        window.open(`http://chatgpt.com/?q=${encodeURIComponent(query)}`);
    } catch (error){
        const query = '[JavaScript] fix error :${error.message}'
        window.open(`http://chatgpt.com/?q=${encodeURIComponent(query)}`);
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

window.playAudio = function (videoElementId) {
    var video = document.getElementById(videoElementId);
    if (video) {
        video.muted = false;
        video.play();
    }
}

function setupCallEvents(localVideoId, remoteVideoId, answerButtonId, callButtonId) {
    var localVideo = document.getElementById(localVideoId);
    var remoteVideo = document.getElementById(remoteVideoId);
    var answerButton = document.getElementById(answerButtonId);
    var callButton = document.getElementById(callButtonId);

    answerButton.addEventListener('click', function () {
        remoteVideo.muted = false;
        localVideo.play();
        remoteVideo.play();
    });

    callButton.addEventListener('click', function () {
        remoteVideo.muted = false;
        localVideo.play();
        remoteVideo.play();
    });
}

