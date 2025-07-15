document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("password");
    const toggleIcon = document.getElementById("togglePassword");
    const confirmPasswordInput = document.getElementById("confirmPassword");
    const toggleIconConfirmPassword = document.getElementById("toggleConfirmPassword");

    toggleIcon.addEventListener("click", function () {
        const isHidden = passwordInput.type === "password";
        passwordInput.type = isHidden ? "text" : "password";
        this.classList.toggle("fa-eye-slash", !isHidden);
        this.classList.toggle("fa-eye", isHidden);
    });

    toggleIconConfirmPassword.addEventListener("click", function () {
        const isHidden = confirmPasswordInput.type === "password";
        confirmPasswordInput.type = isHidden ? "text" : "password";
        this.classList.toggle("fa-eye-slash", !isHidden);
        this.classList.toggle("fa-eye", isHidden);
    });

    $('#username').on('input', function () {
        const val = $(this).val();
        $('#usernameCheck').text(val.length >= 4 ? '✓ Tên hợp lệ' : 'Tên đăng nhập quá ngắn')
            .removeClass('text-danger text-success')
            .addClass(val.length >= 4 ? 'text-success' : 'text-danger');
    });

    $('#email').on('input', function () {
        const val = $(this).val();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        $('#emailCheck').text(emailRegex.test(val) ? '✓ Email hợp lệ' : 'Email không hợp lệ')
            .removeClass('text-danger text-success')
            .addClass(emailRegex.test(val) ? 'text-success' : 'text-danger');
    });

    $('#password').on('input', function () {
        const val = $(this).val();
        const passRegex = /^(?=.*[A-Z])(?=.*[\W_]).+$/;
        $('#passwordCheck').text(passRegex.test(val) ? '✓ Mật khẩu mạnh' : 'Phải có chữ hoa + ký tự đặc biệt')
            .removeClass('text-danger text-success')
            .addClass(passRegex.test(val) ? 'text-success' : 'text-danger');
    });

    $('#confirmPassword').on('input', function () {
        const match = $(this).val() === $('#password').val();
        $('#confirmPasswordCheck').text(match ? '✓ Khớp mật khẩu' : 'Không khớp mật khẩu')
            .removeClass('text-danger text-success')
            .addClass(match ? 'text-success' : 'text-danger');
    });
});
