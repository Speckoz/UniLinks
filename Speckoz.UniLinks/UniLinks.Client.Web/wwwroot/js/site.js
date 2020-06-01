$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

function checkTheme() {
    let themeInCookie = getCookie("theme");
    if (themeInCookie != "") {
        const themeCheckbox = document.getElementById("themeCheckbox");

        if (themeInCookie == "dark") {
            themeCheckbox.setAttribute("checked", "");
        }

        else if (themeInCookie == "light") {
            themeCheckbox.removeAttribute("checked");
        }
        ChangeLinkTheme(`css/theme.${themeInCookie}.css`);
        return;
    }

    setCookie("theme", "light");
}

function ChangeTheme() {
    let themeInCookie = getCookie("theme");

    if (themeInCookie != "") {
        const themeCheckbox = document.getElementById("themeCheckbox");

        if (themeInCookie == "dark") {
            themeCheckbox.removeAttribute("checked");
            themeInCookie = setCookie("theme", "light");
        }

        else if (themeInCookie == "light") {
            themeCheckbox.setAttribute("checked", "");
            themeInCookie = setCookie("theme", "dark");
        }
        ChangeLinkTheme(`css/theme.${themeInCookie}.css`);

        return;
    }

    setCookie("theme", "light");
}

function ChangeLinkTheme(cssFile) {
    document.getElementById("clientThemeLink").setAttribute("href", cssFile);
}

function SendAlert(msg) {
    alert(msg);
}

function ShowModal(modalId) {
    $(document).ready(() => $(`#${modalId}`).modal('show'));
}

function HideModal(modalId) {
    $(document).ready(() => $(`#${modalId}`).modal('hide'));
}

function shoot(time) {
    const video = document.getElementById('video');
    //video.currentTime = time;
    video.currentTime += 0.25;

    const scaleFactor = 0.25;

    const w = video.videoWidth * scaleFactor;
    const h = video.videoHeight * scaleFactor;

    const canvas = document.getElementById('canvasid');
    canvas.width = w;
    canvas.height = h;

    const ctx = canvas.getContext('2d');
    ctx.drawImage(video, 0, 0, w, h);
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cname, value) {
    document.cookie = `${cname}=${value}`;
    return value;
}