window.cockroachOnScreen = false;

function spawnCockroach() {
    window.cockroachOnScreen = true;
}
function killCockroachClient() {
    window.cockroachOnScreen = false;
}

function onCockroachClicked() {

    fetch('/Kebab/KillCockroach', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: '{}'
    })
        .then(response => response.json())
        .then(data => {
            killCockroachClient();
        });
}
