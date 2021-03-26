package com.latihanlks.pc_kab_klaten_joko_supriyanto;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

public class AsetDetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_aset_detail);

        ((Button)findViewById(R.id.btnJual)).setOnClickListener(v -> {
            Intent intent = new Intent(this, JualAsetActivity.class);
            startActivity(intent);
        });
    }
}