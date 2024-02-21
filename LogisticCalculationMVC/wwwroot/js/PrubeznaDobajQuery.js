$(function () {
    // Pole pro ukládání dat z tabulky vstupů
    let inputData = [];
    let rowCount = 1;

    // Funkce pro přidání nového řádku do tabulky vstupů
    function addRowToInputTable(data) {
        let row = '<tr>';
        row += '<td>' + data[0] + '</td>';
        row += '<td contenteditable="true">' + data[1] + '</td>';
        row += '<td contenteditable="true">' + data[2] + '</td>';
        row += '<td contenteditable="true">' + data[3] + '</td>';
        row += '</tr>';
        $('#prubeznaDobaVstup tbody').append(row);
    }

    // Událost pro přidání nového řádku
    $('#pridatPracovisteBtn').on('click', function () {
        let newRowData = [
            inputData.length + 1,
            '',
            '',
            ''
        ];

        // Přidání nového řádku do tabulky vstupů
        addRowToInputTable(newRowData);

        // Přidání nových dat do pole inputData
        inputData.push({
            Pracoviste: newRowData[0],
            OperacniCas: newRowData[1],
            NastavovaciCas: newRowData[2],
            MezioperacniCas: newRowData[3]
        });
    });

    // Funkce pro odebrání posledního řádku z tabulky vstupů
    function removeLastRowFromInputTable() {
        $('#prubeznaDobaVstup tbody tr:last-child').remove();
        inputData.pop(); // Odebrání posledního prvku z pole
    }

    // Událost pro odebrání posledního řádku
    $('#odebratPracovisteBtn').on('click', function () {
        if (inputData.length > 0) {
            removeLastRowFromInputTable();
        }
    });

    // Logika pro sběr dat z tabulky prubeznaDobaVstup
    function collectData() {
        let tableData = [];

        $("#prubeznaDobaVstup tbody tr").each(function () {
            let rowData = [];
            $(this).find('td').each(function () {
                rowData.push($(this).text().trim());
            });
            tableData.push(rowData);
        });

        return tableData;
    }

    // Událost pro výpočet a odeslání dat na server
    $('#vypocitatPrubeznouDobuBtn').on('click', function () {
        let tableData = collectData();
        let systemy = $('#SystemZpracovani').val();
        let davkaQ = $('#DavkaQ').val();
        let davkaQd = $('#DavkaQd').val();

        let inputData = {
            JsonData: tableData,
            Systemy: systemy,
            DavkaQ: davkaQ,
            DavkaQd: davkaQd
        };

        if (!systemy || !davkaQ || !davkaQd || !isValidTableData(tableData)) {
            alert('Prosím vyplňte všechny potřebná data ve správném formátu a přidejte aspoň jeden řádek do tabulky.');
            return;
        }

        console.log('Sending Data: ');
        console.log(inputData);

        $.ajax({
            type: 'POST',
            url: '/Forms/PrubeznaDobaVypocet',
            contentType: 'application/json',
            data: JSON.stringify(inputData),
            success: function (data) {
                console.log("Received Data: ");
                console.log(data);

                // Přístup k vlastnostem uvnitř 'data'
                let rowData = data.data;

                // Přidání nových řádků s výslednými daty do tabulky vystupů
                let newRow = '<tr>' +
                    '<td>' + rowCount + '</td>' +
                    '<td>' + rowData.systemyPrubeznaDoba + '</td>' +
                    '<td>' + rowData.prubeznaDobaVysledek + '</td>' +
                    '<td>' + rowData.pocetPracovist + '</td>' +
                    '<td>' + rowData.pocetPracovniku + '</td>' +
                    '<td>' + rowData.davkaQ + '</td>' +
                    '<td>' + rowData.davkaQd + '</td>' +
                    '</tr>';

                $('#prubeznaDobaVystup tbody').append(newRow);
                rowCount++;
            },
            error: function (error) {
                console.error("Error: ", error);
            }
        });
    });

    // Ověří, že tabulka obsahuje validní data
    function isValidTableData(tableData) {
        if (!Array.isArray(tableData) || tableData.length === 0) {
            return false;
        }
        for (let i = 0; i < tableData.length; i++) {
            let row = tableData[i];

            if (!Array.isArray(row) || row.some(cell => !cell.trim() || isNaN(Number(cell)))) {
                return false;
            }
        }

        return true;
    }
});