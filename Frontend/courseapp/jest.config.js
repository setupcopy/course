/** @type {import('ts-jest/dist/types').InitialOptionsTsJest} */
const { default: tsjPreset } = require('ts-jest/presets');
module.exports = {
  preset: 'ts-jest',		// preset
  testEnvironment: 'node',	// node环境
};