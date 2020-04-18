function toggleDarkLight()
{
    const body = document.getElementById("theme");
    const currentClass = body.className;
    body.className = currentClass == "dark-mode" ? "light-mode" : "dark-mode";
}