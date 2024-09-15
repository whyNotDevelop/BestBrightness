// wwwroot/js/site.js

window.startClock = (elementId) => {
    function updateClock() {
        const now = new Date();
        const timeString = now.toLocaleTimeString();
        document.getElementById(elementId).textContent = timeString;
    }

    setInterval(updateClock, 1000);
    updateClock(); // initial call to set the time immediately
};
