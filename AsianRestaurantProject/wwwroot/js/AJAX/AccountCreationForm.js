function MakeAjaxRequest()
{
	let username = document.forms['AccountCreation'].elements['email']
	let password = document.forms['AccountCreation'].elements['password']
	let confirmationPassword = document.forms['AccountCreation'].elements['confirmPassword']
	
	console.log("ssdew")
	let Credentials = {password: password, username: username,  passwordConfirmation: confirmationPassword};
	$.ajax({
	type:"POST",
		url:"/Home/GetCreationCedentials",
		data: { credentials: Credentials },

	success:function (response)
	{
		if(response == "success")
		{
			window.location("/Home/AddedPage")
		}
		else
		{
			document.getElementById("guideTxt").innerText = response;
		}
	}
	})
}
