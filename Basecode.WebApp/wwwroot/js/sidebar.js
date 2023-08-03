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

    // If the clickedElement is already in a clicked state, return without toggling
    if (clickedElement.classList.contains("clicked")) {
        return;
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

function handleSubMenuItemClick(e) {
    e.stopPropagation();
    const subMenuItem = e.currentTarget;
    const parentNavItem = subMenuItem.closest(".nav-item");

    // Check if the parent nav item has a sub-menu
    if (parentNavItem) {
        const subMenu = parentNavItem.querySelector(".sub-menu");
        if (subMenu) {
            toggleSubMenu({ currentTarget: parentNavItem });
        }
    }

    // Add the "clicked" class to the parent nav-item
    parentNavItem.classList.add("clicked");
}

for (const item of navItems) {
    item.addEventListener("click", toggleSubMenu);

    const subMenuItems = item.querySelectorAll(".sub-menu-item");
    for (const subMenuItem of subMenuItems) {
        subMenuItem.addEventListener("click", handleSubMenuItemClick);
    }
}
// close and open the side bar
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
let overlayOpen = document.querySelector("#sidebarOpen");
let overlayClosed = document.querySelector("#sidebarClosed");
console.log(sidebarBtn);
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
    overlayOpen.classList.toggle("d-none");
    overlayClosed.classList.toggle("d-none");
});