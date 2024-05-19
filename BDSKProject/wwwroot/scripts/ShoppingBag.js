window.addEventListener("load", function () {
    showContent()
})

function showContent() {
    let data = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")));
    let price = JSON.parse(localStorage.getItem("Price " + localStorage.getItem("id")))

    const template = document.querySelector("#temp-row");
    const tableBody = document.querySelector("#items tbody");
    const sum = document.querySelector(".sum")
    sum.textContent = price
    const placeOrd = document.getElementById("placeOr")
    console.log(placeOrd)
    placeOrd.addEventListener("click",async function () {
        await placeOrder()
    })
    for (let i = 0; i < data.length; i++) {
        const clone = template.content.cloneNode(true);
        let item = clone.querySelector("tr");

        const prodName = item.querySelector(".descriptionColumn").querySelector(".itemName")
        prodName.textContent=data[i].prod.name
        let quantityColumn = item.querySelector(".quantityColumn");
        let quantity = quantityColumn.querySelector(".quantity");
        quantity.textContent=data[i].quantity
        let imageColumn = item.querySelector(".imageColumn");
        let image = imageColumn.querySelector(".image");
        
        if (!image.querySelector(".image")) {
            let img = document.createElement("img");
            img.src = "../images/" + data[i].prod.image;
            img.style.setProperty("width","100px")
            image.appendChild(img);
        } else {
            image.querySelector(".image").src = "../images/" + data[i].prod.image;
            image.style.setProperty("width", "100px")
        }
        item.querySelector(".availabilityColumn").textContent = "במלאי";
        item.querySelector(".price").textContent = data[i].prod.price + " ₪";
        let deleteColumn = item.querySelector(".deleteColumn");
        let del = deleteColumn.querySelector(".delBtn");
        del.addEventListener("click", function () {
            deleteProd(data[i])
        })
        let changeQuantityColumn = item.querySelector(".changeQuantityColumn");
        let plus = changeQuantityColumn.querySelector(".plus");
        let minus = changeQuantityColumn.querySelector(".minus");
        plus.addEventListener("click", function () {
            addQuantity(data[i])
        })
        minus.addEventListener("click", function () {
            decQuantity(data[i])
        })
        tableBody.appendChild(clone);
    }
}

function deleteProd(item) {
    let data = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")));
    let price = JSON.parse(localStorage.getItem("Price " + localStorage.getItem("id")))
    price -= item.prod.price*item.quantity
    data = data.filter(i => i.prod.name != item.prod.name)
    localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify(data))
    localStorage.setItem("Price " + localStorage.getItem("id"), price)
    const parentElement = document.querySelector("#items tbody")
    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }
    showContent()
}

function addQuantity(item) {
    console.log("hhh")
    let data = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")));
    let price = JSON.parse(localStorage.getItem("Price " + localStorage.getItem("id")))

    console.log(data)
    for (let i = 0; i < data.length; i++) {
        if (data[i].prod.name == item.prod.name) {
            data[i].quantity += 1
            price += data[i].prod.price
        }
    }
    localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify(data))
    localStorage.setItem("Price " + localStorage.getItem("id"), price)
    const parentElement = document.querySelector("#items tbody")
    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }
    showContent()
}

function decQuantity(item) {
    let data = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")));
    let price = JSON.parse(localStorage.getItem("Price " + localStorage.getItem("id")))

    if (item.quantity == 1) {
        deleteProd(item)
        return
    }
    for (let i = 0; i < data.length; i++) {
        if (data[i].prod.name == item.prod.name) {
            data[i].quantity -= 1
            price -= data[i].prod.price
        }
    }
    localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify(data))
    localStorage.setItem("Price " + localStorage.getItem("id"), price)
    const parentElement = document.querySelector("#items tbody")
    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }
    showContent()
}

async function placeOrder() {
    try {
        let data
        const UserId = localStorage.getItem("id")
        let basket = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")));
        let orderItems = []
        for (let i = 0; i < basket.length; i++) {
            orderItems.push({ ProductId: basket[i].prod.productId, Quantity: basket[i].quantity})
        }
        const order = {
            UserId,
            Price: localStorage.getItem("Price " + UserId),
            orderItems
        }
     
        const res = await fetch("api/Order", {
            method: "POST",
            body: JSON.stringify(order),
            headers: {
                'Content-Type': 'application/json'
            }

        })
        if (res.ok) {
            data = await res.json()
            console.log(data)
            clearBasket()
        }
        else alert("error")
    }
    catch (err) {
    alert(err)
    }
}

function clearBasket() {
    localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify([]))
    localStorage.setItem("Price " + localStorage.getItem("id"), 0)
    const parentElement = document.querySelector("#items tbody")
    while (parentElement.firstChild) {
        parentElement.removeChild(parentElement.firstChild);
    }
    showContent()
}