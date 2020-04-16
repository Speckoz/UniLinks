var changeToDark = {
    toggleDarkLight: function ()
    {
        var body = document.getElementById("theme");
        var currentClass = body.className;
        body.className = currentClass == "dark-mode" ? "light-mode" : "dark-mode";
    }
}