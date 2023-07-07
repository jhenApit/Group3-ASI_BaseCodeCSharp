const navItems = document.querySelectorAll(".nav-item");
let openedSubMenu = null;
let openedArrowIcon = null;

function toggleSubMenu(e) {
    const clickedElement = e.currentTarget;
    const subMenu = clickedElement.querySelector(".sub-menu");
    const arrowIcon = clickedElement.querySelector(".arrow");

    if (openedSubMenu && openedSubMenu !== subMenu) {
        openedSubMenu.classList.remove("active");
        if (openedArrowIcon) {
            openedArrowIcon.classList.remove("rotate");
        }
    }

    if (subMenu && !clickedElement.querySelector(".sub-menu").contains(e.target)) {
        const isSubMenuOpen = subMenu.classList.contains("active");
        closeAllSubMenus();

        if (!isSubMenuOpen) {
            subMenu.classList.add("active");
            openedSubMenu = subMenu;
            if (arrowIcon) {
                arrowIcon.classList.add("rotate");
                openedArrowIcon = arrowIcon;
            }
        } else {
            openedSubMenu = null;
            openedArrowIcon = null;
        }
    }

    for (const item of navItems) {
        if (item !== clickedElement) {
            item.classList.remove("clicked");
        }
    }

    clickedElement.classList.toggle("clicked");
}

function closeAllSubMenus() {
    for (const item of navItems) {
        const subMenu = item.querySelector(".sub-menu");
        const arrowIcon = item.querySelector(".arrow");
        if (subMenu) {
            subMenu.classList.remove("active");
        }
        if (arrowIcon) {
            arrowIcon.classList.remove("rotate");
        }
    }
    openedSubMenu = null;
    openedArrowIcon = null;
}

function changeColorOnHover() {
    this.classList.add("hovered");
}

function revertColorOnHover() {
    this.classList.remove("hovered");
}

for (const item of navItems) {
    item.addEventListener("click", toggleSubMenu);
    item.addEventListener("mouseenter", changeColorOnHover);
    item.addEventListener("mouseleave", revertColorOnHover);
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