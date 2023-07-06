// remove the 'clicked' class to the clicked element
let navItems = document.querySelectorAll(".nav-item");
for (let i = 0; i < navItems.length; i++) {
    navItems[i].addEventListener("click", (e) => {
        let clickedElement = e.currentTarget;

        // remove 'clicked' class from all nav-items except the clicked one
        for (let j = 0; j < navItems.length; j++) {
            if (navItems[j] !== clickedElement) {
                navItems[j].classList.remove("clicked");
            }
        }
        clickedElement.classList.toggle("clicked");
    });
}

// show the content of the markdown upon clicking na arrow icon
let arrow = document.querySelectorAll(".arrow");
for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;
        arrowParent.classList.toggle("showMenu");
    });
}

// close and open the side bar
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
console.log(sidebarBtn);
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});