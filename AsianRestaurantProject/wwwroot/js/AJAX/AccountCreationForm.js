function MakeAjaxRequest(username,password,confirmationPassword)
{
	console.log(username)
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
