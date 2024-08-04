document.addEventListener('DOMContentLoaded', function () {
    initializeAutoNumericDecimal();
    initializeAutoNumericDouble();
    initializeFlatpickrs();
});

function initializeAutoNumericDecimal() {
    var elements = document.getElementsByClassName('autoNumeric');
    for (var i = 0; i < elements.length; i++) {
        var element = elements[i];
        if (!element) continue;

        var classNames = element.className.split(' ');
        var autoNumericClass = classNames.find(className => className.startsWith('autoNumeric-decimal-'));

        if (autoNumericClass) {
            var classParts = autoNumericClass.split('-');
            if (classParts.length < 4) continue;

            var precision = parseInt(classParts[2]);
            var decimalPlaces = parseInt(classParts[3]);

            new AutoNumeric(element, {
                decimalCharacter: ',',
                digitGroupSeparator: '.',
                decimalPlaces: decimalPlaces,
                unformatOnSubmit: true,
                modifyValueOnWheel: false,
                rawValueDivisor: Math.pow(10, precision)
            });
        }
    }
}

function initializeAutoNumericDouble() {
    var elements = document.getElementsByClassName('autoNumeric');
    for (var i = 0; i < elements.length; i++) {
        var element = elements[i];
        if (!element) continue;

        var classNames = element.className.split(' ');
        var autoNumericClass = classNames.find(className => className.startsWith('autoNumeric-double-'));

        if (autoNumericClass) {
            var classParts = autoNumericClass.split('-');
            if (classParts.length < 4) continue;

            var precision = parseInt(classParts[2]);
            var decimalPlaces = parseInt(classParts[3]);

            new AutoNumeric(element, {
                decimalCharacter: ',',
                digitGroupSeparator: '.',
                decimalPlaces: decimalPlaces,
                unformatOnSubmit: true,
                modifyValueOnWheel: false,
                rawValueDivisor: Math.pow(10, precision)
            });
        }
    }
}

function initializeFlatpickrs() {
    const flatpickrElements = document.querySelectorAll('.flatpickr, .flatpickr-datetime');

    flatpickrElements.forEach(function (element) {
        const isWeeknum = element.classList.contains('flatpickr-weeknum');
        const isRange = element.classList.contains('flatpickr-range');
        const dateFormat = element.dataset.dateFormat || "dd-MM-yyyy";

        const config = {
            enableTime: element.classList.contains('flatpickr-datetime'),
            dateFormat: dateFormat,
        };

        if (isWeeknum) {
            config.weekNumbers = true;
        }

        if (isRange) {
            config.mode = "range";
        }

        flatpickr(element, config);
    });
}
