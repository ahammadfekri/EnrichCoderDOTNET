    function animateCounters() {
            const counters = document.querySelectorAll('.counter-number');
    const speed = 200;

            counters.forEach(counter => {
                const updateCount = () => {
                    const target = +counter.getAttribute('data-target');
    const count = +counter.innerText.replace('+', '');
    const increment = target / speed;

    if (count < target) {
        counter.innerText = '+' + Math.ceil(count + increment);
    setTimeout(updateCount, 1);
                    } else {
        counter.innerText = '+' + target;
                    }
                };

                const observer = new IntersectionObserver((entries) => {
                    if (entries[0].isIntersecting) {
        updateCount();
                        observer.disconnect();                     }
                }, {threshold: 0.5 });

    observer.observe(counter.parentElement);
            });
        }
    document.addEventListener('DOMContentLoaded', animateCounters);
