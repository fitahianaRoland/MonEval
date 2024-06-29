const menuContainer = document.getElementById('menuContainer');
const paginationMenu = document.getElementById('paginationMenu');

// Nombre total d'éléments
const totalElementsMenu = menuContainer.querySelectorAll('.item').length;

// Nombre d'éléments par page
const elementsPerPageMenu = 5;

// Nombre total de pages
const totalPagesMenu = Math.ceil(totalElementsMenu / elementsPerPageMenu);

// Fonction pour générer la pagination
function generatePaginationMenu() {
    paginationMenu.innerHTML = ''; // Réinitialise la pagination

    for (let i = 1; i <= totalPagesMenu; i++) {
        const pageLink = document.createElement('span');
        pageLink.textContent = i;
        pageLink.addEventListener('click', function () {
            setActiveSpan(pageLink);
            displayPage(i);
        });
        paginationMenu.appendChild(pageLink);
    }
}

// Afficher la page sélectionnée
function displayPage(pageNumber) {
    const start = (pageNumber - 1) * elementsPerPageMenu;
    const end = start + elementsPerPageMenu;

    const allItems = menuContainer.querySelectorAll('.item');
    allItems.forEach((item, index) => {
        if (index >= start && index < end) {
            item.style.display = 'block'; // Afficher l'élément
        } else {
            item.style.display = 'none'; // Masquer l'élément
        }
    });
}

// Fonction pour ajouter la classe active au span sélectionné
function setActiveSpan(clickedSpan) {
    const allSpans = paginationMenu.querySelectorAll('span');
    allSpans.forEach(span => {
        if (span === clickedSpan) {
            span.classList.add('active');
        } else {
            span.classList.remove('active');
        }
    });
}

// Générer la pagination au chargement de la page
generatePaginationMenu();
displayPage(1); // Afficher la première page par défaut
