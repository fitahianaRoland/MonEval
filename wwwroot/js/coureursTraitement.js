document.getElementById('coureursTraitement').addEventListener('submit', function (event) {
    event.preventDefault();
    const formData = new FormData(event.target);
    const idCoureurs = formData.getAll('id_coureur');
    
    // Encode the id_coureur values to be used in the URL
    const encodedIdCoureurs = idCoureurs.map(id => encodeURIComponent(id)).join('&id_coureur=');
    
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    
    fetch(`../../Etape/EtapeAffectationEquipe?id_coureur=${encodedIdCoureurs}`, {
        method: 'POST',
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            'RequestVerificationToken': token
        }
    })
    .then(response => {
        if (!response.ok) {
            alert("Impossible d'accéder au contrôleur");
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(result => {
        if (result.success) {
            alert(result.message);
            window.location.href = '/Etape/InterfaceEquipPage'; 
        } else {
            alert(result.message);
        }
    })
    .catch(error => {
        console.error('Error:', error);
    });
});
