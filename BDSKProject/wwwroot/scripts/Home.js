const callServer = async () => {
    try {
        const res = await fetch("User")
        if (!res.ok)
            throw new Error("error in fetching data")
        const data = await res.json()
        alert(data)
    }
    catch (ex) {
        alert(ex)
    }
}
const register = async () => {
    try {
        const username = document.getElementById("txtUsername").value
        const password = document.getElementById("password").value
        const firstName = document.getElementById("txtFirstName").value
        const lastName = document.getElementById("txtLastName").value
        const user = {
            username, password, firstName, lastName
        }
        const res = await fetch("api/User/register", {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json'
            }

        })
        if (res.ok) {
            const data = await res.json()

            alert("נרשמת בהצלחה למערכת")
            localStorage.setItem("id", data.id)
            window.location.replace("Products.html")
        }
        else alert("error")
    }
    catch (err) {
        alert(err)
    }
}
const login = async () => {
        try {
            const username = document.getElementById("txtName").value
            const password = document.getElementById("txtPassword").value
            const user = {
                username, password
            }
            const res = await fetch("api/User/login", {
                method: "POST",
                body: JSON.stringify(user),
                headers: {
                    'Content-Type': 'application/json'
                }

            })

            if (res.ok) {
                const data =await res.json()
                localStorage.setItem("id", data.id)
                window.location.replace("Products.html")
            }
            else alert("incorrect username or password")

        }
        catch (error) {
            alert(error)
        }
}

const checkPassword = async () => {
    try {
        const password = document.getElementById("password").value
        const passColor = document.getElementById("password")
            const res = await fetch("api/User/password", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(password)
            })
            if (res.ok) {
                const data = await res.json()
                localStorage.setItem("id", data.id)
                window.location.replace("Products.html")
            }
            else alert("incorrect username or password")
       
        if (data == 0) {
            passColor.style.setProperty("background-color","red")
        }
        else if (data < 2)
            passColor.style.setProperty("background-color", "orange")
        else passColor.style.setProperty("background-color", "green")
    } catch (e) {
        alert("catch  ")
        alert(e)
    }   
}
const update = async () => {
    try {
      

        const id = localStorage.getItem("id");
        const username = document.getElementById("txtUsernameUp").value
        const password = document.getElementById("passwordUp").value
        const firstName = document.getElementById("txtFirstNameUp").value
        const lastName = document.getElementById("txtLastNameUp").value
        const user = {
            username, password, firstName, lastName
        }
        alert("id",id)
        const res = await fetch(`api/User/${id}`, {
            method: "PUT",
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json'
            }

        })
        alert("עודכן")
    }
    catch (err) {
        alert(err)
    }
    
}