window.downloadfile = (filename) => {
    //location.href = '/api/downloads/' + filename;
    //location.href = filename;
    var a = document.createElement("a");
    document.body.appendChild(a);
    a.style = "display: none";

    //var url = window.URL.createObjectURL(blob);
    //a.href = url;
    a.download = filename;
    a.click();
    window.URL.revokeObjectURL(url);
}