let currentBasketItems = {};

function AddItemToBasket(id) {
	let baseElement = document.querySelector("#" + id)
	const data = {
		Quantity: parseInt(baseElement.querySelector("#" + id + " span").innerText),
		Name: baseElement.querySelector(".title").innerText,
		Description: baseElement.querySelector(".item-description").innerText,
		Cost: baseElement.querySelector("p b").innerText,
		Iid: id.slice(4)

	}
	console.log(data)
	if (currentBasketItems[id] == "" || currentBasketItems[id] == undefined) {
		currentBasketItems[id] = data;
	}
	else {
		data.Quantity += currentBasketItems[id].quantity
		console.log
		currentBasketItems[id] = data;
	}

	console.log(JSON.stringify(data));
	document.cookie = "Basket=" + encodeURI(JSON.stringify(data)) + "path=/Home/Basket";
	console.log(document.cookie["Basket"])
	
}

function IncrementCounter(id) {
	let quantity = document.querySelector("#" + id + " span").innerHTML++;
}

function DecrementCounter(id) {
	let quantity = document.querySelector("#" + id + " span").innerHTML --;
}

function setCookie(name, value, days) {
	let expires = "";
	if (days) {
		const date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		expires = "; expires=" + date.toUTCString();
	}
	document.cookie = name + "=" + (value || "") + expires + "; path=/";
}