const navItems = document.querySelectorAll(".nav-item");
let openedSubMenu = null;

function toggleSubMenu(e) {
    const clickedElement = e.currentTarget;
    const subMenu = clickedElement.querySelector(".sub-menu");
    const arrowIcon = clickedElement.querySelector(".arrow");

    if (openedSubMenu && openedSubMenu !== subMenu) {
        openedSubMenu.classList.remove("active");
        if (arrowIcon) {
            arrowIcon.classList.remove("rotate");
        }
    }

    if (subMenu && !clickedElement.querySelector(".sub-menu").contains(e.target)) {
        subMenu.classList.toggle("active");
        openedSubMenu = subMenu.classList.contains("active") ? subMenu : null;
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
    const subMenuItems = item.querySelectorAll(".sub-menu-item");
    for (const subMenuItem of subMenuItems) {
        subMenuItem.addEventListener("click", (e) => {
            e.stopPropagation();
        });
    }
}

// close and open the side bar
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
console.log(sidebarBtn);
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});