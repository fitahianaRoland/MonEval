console.log("pagination")
const tableBody = document.getElementById('tableBody');
const pagination = document.getElementById('pagination');

// Nombre total d'éléments
const totalElements = tableBody.querySelectorAll('tr').length;

// Nombre d'éléments par page
const elementsPerPage = 10;

// Nombre total de pages
const totalPages = Math.ceil(totalElements / elementsPerPage);

// Fonction pour générer la pagination
function generatePagination() {
    pagination.innerHTML = ''; // Réinitialise la pagination

    for (let i = 1; i <= totalPages; i++) {
        const pageLink = document.createElement('span');
        pageLink.textContent = i;
        pageLink.addEventListener('click', function () {
            setActiveSpan(pageLink);
            displayPage(i);
        });
        pagination.appendChild(pageLink);
    }
}

// Afficher la page sélectionnée
function displayPage(pageNumber) {
    const start = (pageNumber - 1) * elementsPerPage;
    const end = start + elementsPerPage;

    const allRows = tableBody.querySelectorAll('tr');
    allRows.forEach((row, index) => {
        if (index >= start && index < end) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

// Fonction pour ajouter la classe active au span sélectionné
function setActiveSpan(clickedSpan) {
    const allSpans = pagination.querySelectorAll('span');
    allSpans.forEach(span => {
        if (span === clickedSpan) {
            span.classList.add('active');
        } else {
            span.classList.remove('active');
        }
    });
}

// Générer la pagination au chargement de la page
generatePagination();
displayPage(1); // Afficher la première page par défaut
