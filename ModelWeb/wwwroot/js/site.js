
var footNode = document.getElementById("footId");
footNode.innerHTML = footNode.innerHTML + "<i>This page was last refreshed on: " + document.lastModified + "</i>";

var menuTextNode = document.getElementsByClassName("menuText");
for (var i = 0; i < menuTextNode.length; i++)
{
    menuTextNode[i].addEventListener("mouseover",
        function () {
            this.style.fontWeight = "bold";
        });
    menuTextNode[i].addEventListener("mouseout",
        function () {
            this.style.fontWeight = "normal";
        });
}

var logoutBtnIdNode = document.getElementById("logoutBtnId");
logoutBtnIdNode.addEventListener("mouseover",
    function () {
        this.style.fontWeight = "bold";
    });
logoutBtnIdNode.addEventListener("mouseout",
    function () {
        this.style.fontWeight = "normal";
    });

var modelWebsiteIdNode = document.getElementById("modelWebsiteId");
modelWebsiteIdNode.addEventListener("mouseover",
    function () {
        this.style.fontWeight = "bold";
    });
modelWebsiteIdNode.addEventListener("mouseout",
    function () {
        this.style.fontWeight = "normal";
    });