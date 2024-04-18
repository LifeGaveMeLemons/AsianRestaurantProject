
  function getCookieValue(cookieName) {
    const name = cookieName + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    console.log(decodedCookie)
    const ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) === 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
  }


function removeBasketItem(iid) {
  let arrayOfItems = JSON.parse(getCookieValue("Basket"));
  console.log(arrayOfItems);
  let filteredItemsArray = arrayOfItems.filter(item => String(item.Iid) !== String(iid));

  console.log(filteredItemsArray);
  document.cookie = "Basket=" + encodeURIComponent(JSON.stringify(Object.values(filteredItemsArray))) + ";";
  document.getElementById(iid).remove();
  calculateTotal(filteredItemsArray);
}

function calculateTotal(filteredItemsArray) {
  let acc = 0;
  console.log(filteredItemsArray)
  for (const value of filteredItemsArray.entries()) {
    console.log(value[1].Quantity);
    acc += Number(value[1].Quantity) * Number(value[1].Cost);
  }
  document.getElementById("sum").innerText = Math.round(acc *100)/100;
  
}
function calculateTotalStartup() {
  let filteredItemsArray = JSON.parse(getCookieValue("Basket"));
  let acc = 0;
  console.log(filteredItemsArray)
  for (const value of filteredItemsArray.entries()) {
    console.log(value[1].Quantity);
    console.log(value[1].Cost)
    console.log(Number(value[1].Quantity) * Number(value[1].Cost))
    acc += Number(value[1].Quantity) * Number(value[1].Cost);
    console.log("accc")
    console.log(acc)
  }
  console.log("hir");
  document.getElementById("sum").innerText = Math.round(acc * 100) / 100;

}

document.addEventListener("DOMContentLoaded", calculateTotalStartup)