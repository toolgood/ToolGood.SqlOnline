(function () {

    var isNode = (typeof module !== 'undefined' && module && module.exports);
    var globalScope = (
        typeof global !== 'undefined'
        && (typeof window === 'undefined' || window === global.window))
        ? global : this;

    var moment;
    if (isNode) {
        moment = require('moment');
    } else {
        moment = globalScope.moment;
    }

    if (typeof moment !== 'function') {
        throw new Error('timediff requires moment.js');
        return;
    }

    function timediff(start, end, options) {
        var now = new Date();
        if (start === 'now') {
            start = now;
        }
        if (end === 'now') {
            end = now;
        }
        start = moment(start);
        if (!start.isValid()) throw 'invalid start date ' + start;
        end = moment(end);
        if (!end.isValid()) throw 'invalid end date ' + end;

        if (options instanceof String || typeof options === 'string') {
            options = { units: options };
        }

        if (typeof options === 'function') {
            options = { callback: options };
        }

        var config = {
            units: {
                years: true,
                months: true,
                weeks: true,
                days: true,
                hours: true,
                minutes: true,
                seconds: true,
                milliseconds: true
            },
            returnZeros: true,
            callback: null
        };

        if (options instanceof Object) {
            if (options.units instanceof String || typeof options.units === 'string') {
                if (options.units.search('Y') === -1) config.units.years = false;
                if (options.units.search('M') === -1) config.units.months = false;
                if (options.units.search('W') === -1) config.units.weeks = false;
                if (options.units.search('D') === -1) config.units.days = false;
                if (options.units.search('H') === -1) config.units.hours = false;
                if (options.units.search('m') === -1) config.units.minutes = false;
                if (options.units.search('S') === -1) config.units.seconds = false;
                if (options.units.search('s') === -1) config.units.milliseconds = false;
            } else if (typeof options.units == 'object') {
                if (!options.units.years) config.units.years = false;
                if (!options.units.months) config.units.months = false;
                if (!options.units.weeks) config.units.weeks = false;
                if (!options.units.days) config.units.days = false;
                if (!options.units.hours) config.units.hours = false;
                if (!options.units.minutes) config.units.minutes = false;
                if (!options.units.seconds) config.units.seconds = false;
                if (!options.units.milliseconds) config.units.milliseconds = false;
            }

            if (options.returnZeros === false) config.returnZeros = false;
            if (typeof options.callback === 'function') config.callback = options.callback;
        }

        var result = {};

        for (var unit in config.units) {
            if (config.units[unit]) {
                var value = end.diff(start, unit);
                start.add(value, unit);
                if (config.returnZeros || value != 0) result[unit] = value;
            }
        }

        if (config.callback) {
            return config.callback(result);
        }

        return result;
    }

    if (isNode) {
        module.exports = timediff;
    } else {
        globalScope.timediff = timediff;
    }
})();