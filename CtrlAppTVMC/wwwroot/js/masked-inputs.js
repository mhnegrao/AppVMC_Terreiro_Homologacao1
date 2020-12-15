/**usado com Cleave.js */
window.maskInputs = () => {
    /**obtém os elementos na página */
    //#region ...elementas da página...
    var datesCollection = document.getElementsByClassName("mask-data");
    var hoursCollection = document.getElementsByClassName("mask-hora");
    var currencyCollection = document.getElementsByClassName("mask-moeda");
    var phoneCollection = document.getElementsByClassName("mask-fone");
    var cpfCnpjCollection = document.getElementsByClassName("mask-cpf-cnpj");
    var cepCollection = document.getElementsByClassName("mask-cep");

    /**converte para Array */
    var dates = Array.from(datesCollection);
    var hours = Array.from(hoursCollection);
    var phones = Array.from(phoneCollection);
    var moedas = Array.from(currencyCollection);
    var ceps = Array.from(cepCollection);
    var cpfs = Array.from(cpfCnpjCollection);
    var activeFormat;
    //#endregion

    //#region ...Máscaras...
    /*formato cpf-cnpj*/
    if (cpfs.length > 0) {
        cpfs.forEach(function (e) {
            $(document).ready(function () {
                $(e).on('keyup', function () {
                    if (activeFormat != null) {
                        activeFormat.destroy();
                        activeFormat = null;
                    }

                    if ($(this).val().length <= 14) {
                        activeFormat = new Cleave(e, {
                            numericOnly: true,
                            blocks: [3, 3, 3, 3],
                            delimiters: ['.', '.', '-']
                        });
                    }
                    else {
                        activeFormat = new Cleave(e, {
                            numericOnly: true,
                            blocks: [2, 3, 3, 4, 2],
                            delimiters: ['.', '.', '/', '-']
                        });
                    }
                })
            })
        });
    };

    /** usa o array para definir uma nova instância para cada input com a mesma classe*/
    /*formato data*/
    if (dates.length > 0) {
        dates.forEach(function (date) {
            new Cleave(date, {
                date: true,
                delimiter: '/',
                datePattern: ['d', 'm', 'Y']
            })
        });
    };
    /*formato fone*/
    if (phones.length > 0) {
        phones.forEach(function (phone) {
            new Cleave(phone, {
                numericOnly: true,
                blocks: [0, 2, 5, 4],
                delimiters: ["(", ") ", "-"],
                rawValueTrimPrefix: true
            })
        });
    };

    if (hours.length > 0) {
        hours.forEach(function (hour) {
            new Cleave(hour, {
                time: true,
                timePattern: ['h', 'm']
            })
        });
    };

    /*formato cpf-cnpj*/
    //if (cpfs.length > 0) {
    //    cpfs.forEach(function (cpf) {
    //        cpf.on('keyup', function () {
    //            if (activeFormat != null) {
    //                activeFormat.destroy();
    //                activeFormat = null;
    //            };

    //            if (cpf.length <= 14) {
    //                new Cleave(cpf, {
    //                    uppercase: true,
    //                    delimiters: ['.', '.', '-'],
    //                    blocks: [3, 3, 3, 2],

    //                });
    //            } else {
    //                new Cleave(cpf, {
    //                    uppercase: true,
    //                    blocks: [2, 3, 3, 4, 2],
    //                    delimiters: ['.', '.', '/', '-']
    //                })
    //            }
    //        });
    //    });
    //};

    /*formato cep*/
    if (ceps.length > 0) {
        ceps.forEach(function (cep) {
            new Cleave(cep, {
                blocks: [5, 3],
                delimiter: '-',
                rawValueTrimPrefix: true
            })
        });
    };
    /*formato moeda*/
    if (moedas.length > 0) {
        moedas.forEach(function (moeda) {
            new Cleave(moeda, {
                numeral: true,
                numeralDecimalScale: 2,
                numeralDecimalMark: ',',
                numeralThousandsGroupStyle: 'thousand',
                numeralIntegerScale: 4,
                stripLeadingZeroes: true,
                delimiter: '.',
                rawValueTrimPrefix: true,
                prefix: 'R$ '
            })
        });
    };
    //#endregion
};