console.log(">>> Script in portal.js running!");

const gearTrigger = document.getElementById('gearTrigger');
const settingsDropdown = document.getElementById('settingsDropdown');

if (gearTrigger && settingsDropdown) {
    gearTrigger.addEventListener('click', () => {
        if (settingsDropdown.style.display === 'block') {
            settingsDropdown.style.display = 'none';
        } else {
            settingsDropdown.style.display = 'block';
        }
    });

    document.addEventListener('click', (e) => {
        if (!gearTrigger.contains(e.target) && !settingsDropdown.contains(e.target)) {
            settingsDropdown.style.display = 'none';
        }
    });
}
