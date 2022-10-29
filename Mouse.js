document.addEventListener('click', function (event) {
    let elem = event.target;
    let jsonObject =
    {
        Key: 'click',
        Value: elem.name || elem.id || elem.tagName || "Unkown"
    };
    window.chrome.webview.postMessage(jsonObject);
});