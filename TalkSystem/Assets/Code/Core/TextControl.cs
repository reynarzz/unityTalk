﻿//MIT License

//Copyright (c) 2020 Reynardo Perez (Reynarz)

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Talk
{
    public class TextControl
    {
        private readonly TextMeshProUGUI _text;
        private const int _quadPoints = 4;
        private Color32 _startColor;

        public TextControl(TextMeshProUGUI text)
        {
            _text = text;
            _startColor = _text.color;

            _text.OnPreRenderText += UpdateHightlight;
        }

        public void SetText(string text)
        {
            ClearColors();

            _text.text = text;
        }

        public void ClearColors()
        {
            var colors = new List<Color32>();
            
            _text.mesh.GetColors(colors);

            for (int i = 0; i < colors.Count; i++)
            {
                colors[i] = Color.clear;
            }

            _text.mesh.SetColors(colors);

            _text.canvasRenderer.SetMesh(_text.mesh);

            _text.color = Color.clear;
        }

        private void UpdateHightlight(TMP_TextInfo textInfo)
        {
            //TODO: update mesh.
        }

        public void ShowChar(int wordIndex, int charIndex, Highlight hightlight)
        {
            var textInfo = _text.textInfo;

            var colors = _text.mesh.colors;

            var wordInfo = textInfo.wordInfo[wordIndex];

            var character = textInfo.characterInfo[wordInfo.firstCharacterIndex];

            int vIndex = character.vertexIndex;

            int quadIndex = charIndex * _quadPoints;

            var color = hightlight != default ? hightlight.Color : _startColor;

            colors[vIndex + 0 + quadIndex] = color;
            colors[vIndex + 1 + quadIndex] = color;
            colors[vIndex + 2 + quadIndex] = color;
            colors[vIndex + 3 + quadIndex] = color;

            _text.mesh.SetColors(colors);

            _text.canvasRenderer.SetMesh(_text.mesh);
        }
    }
}