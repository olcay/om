var PaginationModule = function(urlModule) {

    var init = function(currentPage, pageCount, destinationNav) {
        var pages = pagination(currentPage, pageCount);

        var render = _.template($("#pagination-template").html());

        var hash = urlModule.getCurrentHash();

        $(destinationNav).html(render({ currentPage: currentPage, pageCount: pageCount, pages: pages, currentHash: hash }));
    };

    const getRange = (start, end) => {
        return Array(end - start + 1)
            .fill()
            .map((v, i) => i + start);
    }

    const pagination = (currentPage, pageCount) => {
        var delta;
        if (pageCount <= 7) {
            // delta === 7: [1 2 3 4 5 6 7]
            delta = 7;
        } else {
            // delta === 2: [1 ... 4 5 6 ... 10]
            // delta === 4: [1 2 3 4 5 ... 10]
            delta = currentPage > 4 && currentPage < pageCount - 3 ? 2 : 4;
        }

        const range = {
            start: Math.round(currentPage - delta / 2),
            end: Math.round(currentPage + delta / 2)
        }

        if (range.start - 1 === 1 || range.end + 1 === pageCount) {
            range.start += 1;
            range.end += 1;
        }

        var pages =
            currentPage > delta
                ? getRange(Math.min(range.start, pageCount - delta), Math.min(range.end, pageCount))
                : getRange(1, Math.min(pageCount, delta + 1));

        const withDots = (value, pair) => (pages.length + 1 !== pageCount ? pair : [value]);

        if (pages[0] !== 1) {
            pages = withDots(1, [1, '...']).concat(pages);
        }

        if (pages[pages.length - 1] < pageCount) {
            pages = pages.concat(withDots(pageCount, ['...', pageCount]));
        }

        return pages;
    }

    return {
        init: init
    };
}(URLModule);