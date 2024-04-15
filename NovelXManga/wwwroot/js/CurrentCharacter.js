document.addEventListener("DOMContentLoaded", function () {
    const container = document.querySelector('.CUS-MangaContainerInputQuote');
    if (container) {
        const lineHeight = 15; 
        const observer = new ResizeObserver(entries => {
            for (let entry of entries) {
                const rows = Math.ceil(entry.target.scrollHeight / lineHeight);
                container.style.height = `${rows * lineHeight}px`; 
            }
        });
        observer.observe(container);
    }
});