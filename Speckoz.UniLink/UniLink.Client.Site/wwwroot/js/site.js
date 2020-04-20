function ChangeToLight()
{
    ChangeLinkTheme("css/theme.light.css");
}
function ChangeToDark()
{
    ChangeLinkTheme("css/theme.dark.css");
}

function ChangeLinkTheme(cssFile)
{
    const oldLink = document.getElementById("clientThemeLink");

    const newLink = document.createElement("link");
    newLink.setAttribute("id", "clientThemeLink");
    newLink.setAttribute("rel", "stylesheet");
    newLink.setAttribute("href", cssFile);
    newLink.onload = () =>
    {
        oldLink.parentElement.removeChild(oldLink);
    };

    document.getElementsByTagName("head")[0].appendChild(newLink);
}

function SendAlert(msg)
{
    alert(msg);
}

function ShowModal(modalId)
{
    $(document).ready(function ()
    {
        $('#' + modalId).modal('show');
    });
}

function DisposeModal(modalId)
{
    $(document).ready(function ()
    {
        $('#' + modalId).modal('dispose');
    });
}