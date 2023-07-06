const navItems = document.querySelectorAll(".nav-item");

function toggleSubMenu(e) {
    const clickedElement = e.currentTarget;
    const subMenu = clickedElement.querySelector(".sub-menu");
    const arrowIcon = clickedElement.querySelector(".arrow");

    if (subMenu) {
        subMenu.classList.toggle("active");
    }

    for (const item of navItems) {
        if (item !== clickedElement) {
            item.classList.remove("clicked");
        }
    }

    clickedElement.classList.toggle("clicked");

    if (arrowIcon) {
        arrowIcon.classList.toggle("rotate");
    }
}

for (const item of navItems) {
    item.addEventListener("click", toggleSubMenu);
}

// close and open the side bar
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
console.log(sidebarBtn);
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});