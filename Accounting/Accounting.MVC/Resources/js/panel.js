document.addEventListener("DOMContentLoaded", function () {
    let nodes = document.querySelectorAll(".nav-item");
    let controllerName =
        window.location.pathname
            .split("/")[1]
            .replaceAll("/", "")
            .toLowerCase();

    let menuLinks = document.querySelectorAll(".nav-pills .nav-link p");
    menuLinks.forEach(node => {
        let nodeText =
            node.dataset
                .controller
                ?.trim()
                ?.replaceAll(" ", "")
                ?.toLowerCase();

        if(nodeText === controllerName){
            SetParentState(node);
        }
    })
});


function SetParentState(node){
    if(node === null || node.nodeName === 'NAV'){
        return;
    }
    if(node.classList.contains('nav-item')){
        node.classList.add('menu-open');
        node.querySelector('.nav-link').classList.add('active');
        let treeView = node.querySelector('.nav-treeview');
        if(treeView !== null) {
            treeView.style.display = 'block'
        }
    }
    return SetParentState(node.parentElement)
}