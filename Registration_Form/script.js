$(document).ready(function() {

  // Toggle password visibility
  $(".toggle-password").click(function() {
    let input = $($(this).attr("toggle"));
    if (input.attr("type") === "password") {
      input.attr("type", "text");
      $(this).html('<i class="eye-icon">🙈</i>'); // eye close
    } else {
      input.attr("type", "password");
      $(this).html('<i class="eye-icon">👁️</i>'); // eye open
    }
  });

  // Registration form validation
  $('#registrationForm').on('submit', function(e) {
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
      showError('Password must have 1 uppercase, 1 number, 1 special character, and minimum 6 characters.');
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
    var pattern = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
    return pattern.test(password);
  }

});
