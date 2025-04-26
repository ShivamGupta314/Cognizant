$(document).ready(function () {
    $('#registrationForm').on('submit', function (e) {
      e.preventDefault();
  
      let name = $('#name').val().trim();
      let email = $('#email').val().trim();
      let password = $('#password').val();
      let confirmPassword = $('#confirmPassword').val();
  
      if (name.length < 3) {
        showError('Username must be at least 3 characters.');
      } else if (!validateEmail(email)) {
        showError('Invalid email format.');
      } else if (!validatePassword(password)) {
        showError('Password must be at least 6 characters and contain at least 1 uppercase letter, 1 number, and 1 special character.');
      } else if (password !== confirmPassword) {
        showError('Passwords do not match.');
      } else {
        $('#errorMsg').css('color', 'green').text('🎉 Registration Successful!');
        $('#registrationForm')[0].reset();
      }
    });
  
    function showError(message) {
      $('#errorMsg').css('color', 'red').text(message);
    }
  
    function validateEmail(email) {
      var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return re.test(email.toLowerCase());
    }
  
    function validatePassword(password) {
      // Minimum 6 chars, 1 uppercase, 1 digit, 1 special char
      const pattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
      return pattern.test(password);
    }
  });
  