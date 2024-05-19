let categoryChecked = []
let n =""
let min = 0
let max = 100000
let catString = ""

window.addEventListener("load", function () {
    getProducts()
    allCatgories()
    countBasket()
})

async function getProducts() {
    try {
        const res = await fetch("api/Product")
        console.log(res)
        if (res.ok) {
            const data = await res.json()
            console.log(data)
            if (data) {
                showContent(data)
                }
            }
        else alert("error")
        if (!localStorage.getItem("Basket " + localStorage.getItem("id"))) {
            localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify([]))
            localStorage.setItem("Price " + localStorage.getItem("id"), 0)
        }
           
            
    }
    catch (error) {
        alert(error)
    }
}

async function addToBasket(prod) {
    console.log(prod)
    let basket = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")))
    let price = JSON.parse(localStorage.getItem("Price " + localStorage.getItem("id")))
    let i=0
    for ( i; i < basket.length; i++) {
        if (basket[i].prod.name == prod.name) {
            basket[i].quantity += 1
            price+=basket[i].prod.price
            break
        }
    }
    
    if (i == basket.length) {
        await basket.push({ prod, quantity: 1 })
        price += prod.price
        
    }
    localStorage.setItem("Basket " + localStorage.getItem("id"), JSON.stringify(basket)) 
    localStorage.setItem("Price " + localStorage.getItem("id"), price)
    const num = Number(document.querySelector("#ItemsCountText").innerHTML)
    document.querySelector("#ItemsCountText").textContent=num+1
}
function showContent(data) {
    const template = document.querySelector("#temp-card");
    for (let i = 0; i < data.length; i++) {
        const clone = template.content.cloneNode(true);
        console.log("clone" + clone)
        let item = clone.querySelector("div");
        item.querySelector("h1").textContent = data[i].name;
        item.querySelector(".description").textContent = data[i].description;
        item.querySelector(".price").textContent = " מחיר: " + data[i].price + " ₪ ";
        item.querySelector("img").src="../images/"+data[i].image

        var addBasket = clone.querySelector('button');
        console.log(addBasket)
         addBasket.addEventListener('click', function () {
             addToBasket(data[i])
        })
        document.getElementById("ProductList").appendChild(clone);
    }
}
const addCheckBoxEventListeners=async()=> {
    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function (event) {            
            if (event.target.checked) {
                categoryChecked.push(event.target.value)
                console.log(`Checkbox with value ${event.target.value} changed`);
                filterProducts()
            } else {
                categoryChecked = categoryChecked.filter(ct => ct != event.target.value)
                console.log(`Checkbox with value ${event.target.value} changed back`, categoryChecked);
                catString = "";
                filterProducts()
            }
        });
    });
};
async function allCatgories() {
    const resp = await fetch("api/Category");
    const data = await resp.json();
    const template = document.querySelector("#temp-category");
    const categoryList = document.getElementById("categoryList");

    for (let i = 0; i < data.length; i++) {
        const clone = template.content.cloneNode(true);
        const item = clone.querySelector("div");
        item.querySelector(".opt").value = data[i].categoryId;
        item.querySelector(".OptionName").textContent = data[i].categoryName;
        categoryList.appendChild(clone);
    }
    addCheckBoxEventListeners();
}


const findNameEvent =async () => {
    n = document.getElementById("nameSearch").value
    await filterProducts()
};

const minMaxEvent =async () => {
    min = document.getElementById("minPrice").value
    max = document.getElementById("maxPrice").value
    await filterProducts()
};


async function filterProducts() {
    console.log("in")
    try {
        for (let r = 0; r <categoryChecked.length; r++) {
            catString += `&categoryIds=${categoryChecked[r]}`
            console.log("d"+catString)
        }
        console.log(catString)
        const resp = await fetch(`api/Product?name=${n}&min=${min}&max=${max}${catString}`);
        if (resp.ok) {
            const data = await resp.json();
            const parentElement = document.getElementById('ProductList');
            while (parentElement.firstChild) {
                parentElement.removeChild(parentElement.firstChild);
            }
            showContent(data)
            
        }
        else alert("filter failed")
    } catch (e) {
        alert(e)
    }
}
async function cleanDom() {
    n = ""
    categoryChecked = []
    min = null
    max = null
    window.cleanDom()
}

function countBasket() {
    let basket = JSON.parse(localStorage.getItem("Basket " + localStorage.getItem("id")))
    let count=0
    for (let i = 0; i < basket.length; i++) {
        count+=basket[i].quantity
    }
    document.querySelector("#ItemsCountText").textContent = count
}