let currentBasketItems = {};

let isCookieBeinaltered = false;

let finishedInit = false;
function getCookieValue() {
	let temp;
	const name = "Basket=";
	const decodedCookie = decodeURIComponent(document.cookie);
	console.log(decodedCookie)
	const ca = decodedCookie.split(';');
	for (let i = 0; i < ca.length; i++) {
		let c = ca[i];
		while (c.charAt(0) === ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) === 0) {
			temp = JSON.parse(c.substring(name.length, c.length));
		}
		else {
			temp = {};
;
		}
	}
	if (Array.isArray(temp)) {
		currentBasketItems = temp.reduce((obj, item) => {
			// Use the Iid property as the key for the object
			obj[item.Iid] = item;
			return obj;
		}, {});
	} 
	console.log(currentBasketItems)
	finishedInit = true;
	
}
function AddItemToBasket(id) {
	if (!finishedInit) {
		return;
	}
	if (isCookieBeinaltered == true) {
		return;
	}
	isCookieBeinaltered = true;
	let baseElement = document.querySelector("#" + id)
	const data = {
		Quantity: parseInt(baseElement.querySelector("#" + id + " span").innerText),
		Name: baseElement.querySelector(".title").innerText,
		Description: baseElement.querySelector(".item-description").innerText,
		Cost: baseElement.querySelector("p b").innerText.substring(1),
		Iid: id.slice(4)

	}
	console.log("{IID is}"+ String(data.iid))

	if (currentBasketItems[data.Iid] == "" || currentBasketItems[data.Iid] == undefined) {
		console.log("undef or empty")
		console.log(currentBasketItems[data.Iid])
		currentBasketItems[data.Iid] = null;
		currentBasketItems[data.Iid] = data;
	}
	else {
		console.log("quanr")
		data.Quantity += currentBasketItems[data.Iid].Quantity

		currentBasketItems[data.Iid] = data;
	}
	isCookieBeinaltered = false;
	console.log(currentBasketItems)
	let entries = currentBasketItems;
	console.log("ygw48rfygerf6g")
	console.log(JSON.stringify(currentBasketItems));
	console.log("Basket=" + encodeURIComponent(JSON.stringify(currentBasketItems)));
	document.cookie = "Basket=" + encodeURIComponent(JSON.stringify(Object.values(currentBasketItems))) + ";";
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

getCookieValue()