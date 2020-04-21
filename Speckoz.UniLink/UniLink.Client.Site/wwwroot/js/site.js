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
    document.getElementById("clientThemeLink").setAttribute("href", cssFile);
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