const usernameDropdownField = document.getElementById("username");
if (usernameDropdownField) {
    usernameDropdownField.addEventListener("click", () => {
        const authNav = document.getElementById("auth-nav");
        if (authNav.style.display === "block") {
            document.getElementById("auth-nav").style.display = "none";
        } else {
            document.getElementById("auth-nav").style.display = "block";
        }
    });
}
