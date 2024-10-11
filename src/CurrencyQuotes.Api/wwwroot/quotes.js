const desiredSymbols = 'usd,btc,brl,gbp,aud';

document.addEventListener('DOMContentLoaded', async () => {
    const $symbolsEl = document.getElementById('symbols');
    $symbolsEl.addEventListener('change', async function (e) {
        await showQuotesAsync(this.value, desiredSymbols);
    });
});

async function showQuotesAsync(baseSymbol, compareSymbols) {
    // disable "select" element
    const $symbolsEl = document.getElementById('symbols');
    $symbolsEl.setAttribute('disabled', true);

    // try to fetch quotes
    let quotes;
    try {
        quotes = await getQuotesAsync(baseSymbol, compareSymbols);
    }
    catch (err) {
        showError('خطا در دریافت نرخ‌های تبدیل');
        $symbolsEl.removeAttribute('disabled');
        return;
    }

    // convert qoutes to an array.
    const rates = Object.getOwnPropertyNames(quotes).map(propName => ({
        symbol: propName,
        quote: quotes[propName],
    }));

    createTableBody(rates);
    $symbolsEl.removeAttribute('disabled'); // enable "select" element
    hideError();
}

async function getQuotesAsync(baseSymbol, compareSymbols) {
    const res = await fetch(
        `https://localhost:7292/api/currency/quotes/${baseSymbol}?symbols=${compareSymbols}`
    );

    if (!res.ok)
        throw 'response not ok!';

    const quotes = await res.json();
    return quotes.rates;
}

// create html table

function createTableBody(rates) {
    const docFrag = new DocumentFragment();
    for (const rate of rates)
        docFrag.appendChild(createTableRow(rate));

    clearTableBody();
    const $tableBody = document.getElementById('compare-symbols');
    $tableBody.appendChild(docFrag);
}

function clearTableBody() {
    const $tableBody = document.getElementById('compare-symbols');
    $tableBody.innerHTML = '';
}

function createTableRow(rate) {
    const tr = document.createElement('tr');
    tr.appendChild(createTableCell(rate.symbol));
    tr.appendChild(createTableCell(rate.quote));
    return tr;
}

function createTableCell(text) {
    const td = document.createElement('td');
    td.innerHTML = text;
    return td;
}

// toggle error message

function showError(msg) {
    const $errorEl = document.getElementById('error');
    $errorEl.innerHTML = msg;
    $errorEl.classList.remove('hidden');
}

function hideError() {
    const $errorEl = document.getElementById('error');
    $errorEl.innerHTML = '';
    $errorEl.classList.add('hidden');
}
