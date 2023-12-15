function SortBatch(Coloumn) {
    var BatchTable, Rows, ColSorting, i, row1, row2, Sort, Direction, Count;
    Count = 0;
    BatchTable = document.getElementById("BatchTable");
    BatchTable.find('tr').sort(function (a, b) {
        row1 = a.find('td:eq(' + Coloumn + ')').test();
        row1 = b.find('td:eq(' + Coloumn + ')').test();
    })
    ColSorting = true;
    Direction = "Ascending";
    Rows = BatchTable.rows;
}

