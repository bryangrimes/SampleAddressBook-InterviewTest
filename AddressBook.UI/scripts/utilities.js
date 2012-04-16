/// <reference name="MicrosoftAjax.js"/>
function switchViews(obj) {
    var div = document.getElementById(obj);
    var img = document.getElementById('img' + obj);

    if (div.style.display == "none") {
        div.style.display = "inline";
        img.src = "resources/down.png";
    }
    else {
        div.style.display = "none";
        img.src = "resources/right.png";
    }
}

function markfav(addrId, userid) {
    // with the addrId, update bool val accordingly.
    PageMethods.MarkAddressAsFavorite(addrId, userid, OnSuccessFav, OnError);
}

function ConfirmDelete() {
    return confirm('Are you sure you want to delete?');
}

function deleteUser(id) {
    if (ConfirmDelete()) {
        PageMethods.DeleteUser(id, OnSuccessDelete, OnError);
    }
}

function OnSuccessDelete(result, userContext, methodName) {
    alert("item deleted");
    //PageMethod.ReloadGrid();
}

function raiseAsyncPostback() {
    __doPostBack("<%= this.Button1.UniqueID %>", "");
}

function OnSuccessFav(result, userContext, methodName) {
    alert("favorite saved");
    //PageMethod.ReloadGrid();
    __doPostBack('updatePanel.ClientID', '');
}

function OnError(error, userContext, methodName) {
    alert("error from service: " + error.get_message());
}

function PopContactPage(id) {
    if(id == undefined)
        id = 0;

    var newwindow;
    newwindow = window.open('Contact.aspx?uid=' + id, "Contact", "toolbar=no,location=no,scrollbars=yes,statusbar=no,height=600,width=560,resizable=yes");
    if (window.focus) {
        newwindow.focus()
    }
}