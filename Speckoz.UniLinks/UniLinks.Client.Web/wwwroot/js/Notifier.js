/*
 * Nofifier
 *
 * @version 1.2.3
 *
 * @author Javier Sanahuja Liebana <bannss1@gmail.com>
 *
 * https://github.com/jsanahuja/Notifierjs
 *
 */
(function(root, factory) {
    if (typeof define === "function" && define.amd) {
        define([], factory);
    } else if (typeof exports === "object") {
        module.exports = factory();
    } else {
        root.Notifier = factory();
    }
}(this, function() {
    var defaults = {
        autopush: true,
        zindex: 9999,
        default_time: 4500,
        vanish_time: 300,
        fps: 30,
        position: 'bottom-right',
        direction: 'bottom',
        success: {
            classes: 'notifyjs-success',
            textColor: "#155724",
            borderColor: "#c3e6cb",
            backgroundColor: "#d4edda",
            progressColor: "#155724",
            iconColor: "#155724",
            // iconClasses: "",
            icon: '<svg xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8"><path d="M6.41 0l-.69.72-2.78 2.78-.81-.78-.72-.72-1.41 1.41.72.72 1.5 1.5.69.72.72-.72 3.5-3.5.72-.72-1.44-1.41z" transform="translate(0 1)" /></svg>'
        },
        error: {
            classes: 'notifyjs-danger',
            textColor: "#721c24",
            borderColor: "#f5c6cb",
            backgroundColor: "#f8d7da",
            progressColor: "#721c24",
            iconColor: "#721c24",
            // iconClasses: "",
            icon: '<svg xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8"><path d="M1.41 0l-1.41 1.41.72.72 1.78 1.81-1.78 1.78-.72.69 1.41 1.44.72-.72 1.81-1.81 1.78 1.81.69.72 1.44-1.44-.72-.69-1.81-1.78 1.81-1.81.72-.72-1.44-1.41-.69.72-1.78 1.78-1.81-1.78-.72-.72z" /></svg>'
        },
        warning: {
            classes: 'notifyjs-warning',
            textColor: "#856404",
            borderColor: "#fff3cd",
            backgroundColor: "#ffeeba",
            progressColor: "#856404",
            iconColor: "#856404",
            // iconClasses: "",
            icon: '<svg xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8"><path d="M3.09 0c-.06 0-.1.04-.13.09l-2.94 6.81c-.02.05-.03.13-.03.19v.81c0 .05.04.09.09.09h6.81c.05 0 .09-.04.09-.09v-.81c0-.05-.01-.14-.03-.19l-2.94-6.81c-.02-.05-.07-.09-.13-.09h-.81zm-.09 3h1v2h-1v-2zm0 3h1v1h-1v-1z" /></svg>'
        },
        info: {
            classes: 'notifyjs-info',
            textColor: "#0c5460",
            borderColor: "#d1ecf1",
            backgroundColor: "#bee5eb",
            progressColor: "#0c5460",
            iconColor: "#0c5460",
            // iconClasses: "",
            icon: '<svg xmlns="http://www.w3.org/2000/svg" width="8" height="8" viewBox="0 0 8 8"><path d="M3 0c-.55 0-1 .45-1 1s.45 1 1 1 1-.45 1-1-.45-1-1-1zm-1.5 2.5c-.83 0-1.5.67-1.5 1.5h1c0-.28.22-.5.5-.5s.5.22.5.5-1 1.64-1 2.5c0 .86.67 1.5 1.5 1.5s1.5-.67 1.5-1.5h-1c0 .28-.22.5-.5.5s-.5-.22-.5-.5c0-.36 1-1.84 1-2.5 0-.81-.67-1.5-1.5-1.5z" transform="translate(2)"/></svg>'
        }
    };

    var Notification = function(notifier, msg, type, time, vanish, fps, callback) {
        this.pushed = false;

        // Notification
        this.element = document.createElement('div');
        this.element.className = type.className || "";
        this.element.style.display = "none";
        this.element.style.position = "relative";
        this.element.style.padding = "1em 2em 1em 2.5em";
        switch(notifier.options.direction){
            case "top":
                this.element.style.marginTop = "0.5em";
                break;
            case "bottom":
            default:
                this.element.style.marginBottom = "0.5em";
                break;
        }
        this.element.style.width = "100%";
        this.element.style.borderWidth = "1px";
        this.element.style.borderStyle = "solid";
        this.element.style.borderColor = type.borderColor;
        this.element.style.boxSizing = "border-box";
        this.element.style.backgroundColor = type.backgroundColor;

        // Icon
        if(typeof type.icon !== "undefined"){
            var icon = document.createElement('div');
            icon.style.position = "absolute";
            icon.style.top = "50%";
            icon.style.left = "10px";
            icon.style.transform = "translateY(-50%)";
            icon.innerHTML = type.icon;
            if(type.icon.indexOf("<img") != -1){
                icon.childNodes[0].style.width = "16px";
                icon.childNodes[0].style.height = "16px";
            }else if(type.icon.indexOf("<svg ") != -1){
                icon.childNodes[0].style.width = "16px";
                icon.childNodes[0].style.height = "16px";
                if(typeof type.iconColor !== "undefined"){
                    icon.childNodes[0].style.fill = type.iconColor;
                }
            }
            if(typeof type.iconClasses !== "undefined"){
                icon.childNodes[0].className += type.iconClasses;
            }
            this.element.appendChild(icon);
        }

        // Text
        var text = document.createElement('span');
        text.style.color = type.textColor;
        text.innerHTML = msg;
        this.element.appendChild(text);

        // Progress
        var progress = document.createElement('p');
        progress.className = 'progress';
        progress.style.position = "absolute";
        progress.style.bottom = 0;
        progress.style.left = 0;
        progress.style.right = "100%";
        progress.style.height = "2px";
        progress.style.content = " ";
        progress.style.backgroundColor = type.progressColor;
        progress.style.marginBottom = 0;
        this.element.appendChild(progress);


        switch(notifier.options.direction){
            case "top":
                notifier.container.insertBefore(this.element, notifier.container.childNodes[0]);
                break;
            case "bottom":
            default:
                notifier.container.appendChild(this.element);
                break;
        }

        // Callback
        this.callback = callback;

        var self = this;

        this.push = function() {
            if (self.pushed) return;
            self.pushed = true;

            var i = 0,
                lapse = 1000 / fps;

            self.element.style.display = "block";
            self.interval = setInterval(function() {
                i++;
                var percent = (1 - lapse * i / time) * 100;

                progress.style.right = percent + '%';

                if (percent <= 0) {
                    if (typeof callback === 'function') {
                        callback();
                    }
                    self.clear();
                }
            }, lapse);
        };

        this.clear = function() {
            if (!self.pushed) return;

            var lapse = 1000 / fps,
                cut = 1 / (vanish / lapse),
                opacity = 1;

            if (typeof self.interval !== 'undefined') {
                clearInterval(self.interval);
            }
            self.interval = setInterval(function() {
                opacity -= cut;
                self.element.style.opacity = opacity;

                if (opacity <= 0) {
                    clearInterval(self.interval);
                    self.destroy();
                }
            }, lapse);
        };

        this.destroy = function() {
            if (!self.pushed) return;
            self.pushed = false;

            if (typeof self.interval !== 'undefined') {
                clearInterval(self.interval);
            }
            notifier.container.removeChild(self.element);
        };
    };

    var Notifier = function(opts) {
        this.options = Object.assign({}, defaults);
        this.options = Object.assign(this.options, opts);

        this.container = document.getElementById("notifyjs-container-" + this.options.position);
        if(this.container === null){
            this.container = document.createElement('div');
            this.container.id = "notifyjs-container-" + this.options.position;
            this.container.style.zIndex = this.options.zindex;
            this.container.style.position = "fixed";
            this.container.style.maxWidth = "304px";
            this.container.style.width = "100%";
            
            switch(this.options.position){
                case "top-left":
                    this.container.style.top = 0;
                    this.container.style.left = "0.5em";
                    break;
                case "top-right":
                    this.container.style.top = 0;
                    this.container.style.right = "0.5em";
                    break;
                case "bottom-left":
                    this.container.style.bottom = 0;
                    this.container.style.left = "0.5em";
                    break;
                case "bottom-right":
                default:
                    this.container.style.bottom = 0;
                    this.container.style.right = "0.5em";
                    break;
            }
            document.getElementsByTagName('body')[0].appendChild(this.container);
        }


        this.notify = function(type, msg, time, callback) {
            if (typeof this.options[type] === 'undefined') {
                console.error("Notify.js: Error, undefined '" + type + "' notification type");
                return;
            }
            if (typeof time === 'undefined') {
                time = this.options.default_time;
            }

            var notification = new Notification(
                this,
                msg,
                this.options[type],
                time,
                this.options.vanish_time,
                this.options.fps,
                callback
            );
            if(this.options.autopush){
                notification.push();
            }
            return notification;
        };
    };

    return Notifier;
}));