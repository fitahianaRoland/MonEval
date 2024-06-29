document.getElementById('montantTraitement').addEventListener('submit', function (event) {
    event.preventDefault();
    const formData = new FormData(event.target);
    const data = {
        Montant: parseFloat(formData.get('montant')),
        DateNaissance: formData.get('dateNaissance')
    };
    console.log(data.Montant);
    console.log(data.DateNaissance);
    fetch('Payement/PayementTraitement?montant='+encodeURIComponent(data.Montant)+'&dateNaissance='+encodeURIComponent(data.DateNaissance), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest',
            'RequestVerificationToken': '@Html.AntiForgeryToken()'
        },
        // body: JSON.stringify(data)
    })
    .then(response => response.json())
    .then(result => {
        const resultDiv = document.getElementById('loginResult');
        if (result.success) {
            // Redirigez vers un autre contrôleur en cas de succès
            // window.location.href = '@Url.Action(PayementPage, Payement)';
            resultDiv.innerHTML = '<p style="color: green;">' + result.message + '</p>';
        } else {
            resultDiv.innerHTML = '<p style="color: red;">' + result.message + '</p>';
        }
    })
    .catch(error => {
        console.error('Error:', error);
    });
});