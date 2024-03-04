

function CreateUserWithPassword() {
	event.preventDefault()

	let password = document.forms['AccountCreation'].elements['password'].value;
	let confirmPassword = document.forms['AccountCreation'].elements['confirmPassword'].value;
	let url = document.forms['AccountCreation'].elements['url'].value;
	let isvlaid = true;
	console.log(password)
	passwordErrorMessages = "";
	//validate
	console.log(password.length)
	if (password.length < 10) {
		passwordErrorMessages += "your password must be at least 10 characters in length <br>";
		document.getElementById("password").style.borderColor = "red"
	}
	console.log(/\d/.test(password))
	if (!/\d/.test(password)) {
		passwordErrorMessages += "your password must contain numeric characters<br>";
	}
	console.log(/[^\w\s]/.test(password))
	if (!/[^\w\s]/.test(password)) {
		passwordErrorMessages += "your password must contain symbols<br>";
	}
	if (password != confirmPassword) {
		passwordErrorMessages += "your confirmation password muct match your password<br>";
	}
	else {

	}
	console.log(passwordErrorMessages)
	document.forms['AccountCreation'].elements['passwordValidation'].innerHTML = passwordErrorMessages;

	//let credentials = { Email: email };
	//$.ajax({
	//	type: "POST",
	//	url: "/Home/SendVerificationEmail",
	//	data: { credentials: credentials },

	//	success: function (response) {
	//		if (response == "200") {
	//			document.getElementById("errorMessage").innerText = "success"
	//			console.log("success")
	//			console.log(response)
	//		}
	//		else {
	//			document.getElementById("errorMessage").innerText = "fail"
	//			console.log("fail")
	//			console.log(response)
	//		}
	//		return false;
	//	}
	//})
}
