function bindDatePicker(target) {
    if (isIphone()) {
        target.scroller({ preset: 'date', dateOrder: 'mmD ddyy', dateFormat: 'yy-mm-dd', theme: 'ios' });
    }
    else {
        target.scroller({ preset: 'date', dateOrder: 'mmD ddyy', dateFormat: 'yy-mm-dd', theme: 'android-ics light' });
    }
}