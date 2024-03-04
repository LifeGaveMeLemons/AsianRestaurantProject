

function MakeAjaxRequest(e)
{
	event.preventDefault();
	let email = document.forms['VerifyEmail'].elements['email'].value;
	let credentials = { Email: email };
	console.log("dh")
	$.ajax({
	type:"POST",
		url:"/Home/SendVerificationEmail",
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
