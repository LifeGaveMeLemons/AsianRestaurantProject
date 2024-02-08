

function MakeAjaxRequest()
{
	event.preventDefault()

	let email = document.forms['AccountCreation'].elements['email'].value
	let password = document.forms['AccountCreation'].elements['password'].value
	let confirmationPassword = document.forms['AccountCreation'].elements['confirmPassword'].value
	let name = document.forms['AccountCreation'].elements['foreName'].value
	let lastname = document.forms['AccountCreation'].elements['lastName'].value
	console.log(email)
	console.log(password)
	let credentials = { Password: password, Email: email, Forename: name, Lastname: lastname };
	$.ajax({
	type:"POST",
		url:"/Home/GetCreationCredentials",
		data: { credentials: credentials },

	success:function (response)
	{
		if(response == "200")
		{
			document.getElementById("errorMessage").innerText = "success"
			console.log("success")
			console.log(response)
		}
		else
		{
			document.getElementById("errorMessage").innerText = "fail"
			console.log("fail")
			console.log(response)
		}
		return false;
	}
	})
}
