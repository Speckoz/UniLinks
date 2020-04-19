function ChangeToLight()
{
    document.getElementById("theme").className = "light-mode";
}
function ChangeToDark()
{
    document.getElementById("theme").className = "dark-mode";
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