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

function capture(video, scaleFactor)
{
    const w = video.videoWidth * scaleFactor;
    const h = video.videoHeight * scaleFactor;

    const canvas = document.getElementById('canvasid');
    canvas.width = w;
    canvas.height = h;

    const ctx = canvas.getContext('2d');
    ctx.drawImage(video, 0, 0, w, h);

    const img = document.getElementById('canvasimg');
    img.setAttribute('crossOrigin', 'anonymous');
    //img.src = canvas.toDataURL('png', 0.8);
}

function shoot(time)
{
    const video = document.getElementById('video');
    video.currentTime += 0.25;

    capture(video, 0.25);
}