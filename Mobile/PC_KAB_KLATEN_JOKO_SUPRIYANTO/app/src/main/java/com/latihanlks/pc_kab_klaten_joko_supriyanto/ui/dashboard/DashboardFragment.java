package com.latihanlks.pc_kab_klaten_joko_supriyanto.ui.dashboard;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

import com.latihanlks.pc_kab_klaten_joko_supriyanto.DetailProductActivity;
import com.latihanlks.pc_kab_klaten_joko_supriyanto.R;

public class DashboardFragment extends Fragment {

    private DashboardViewModel dashboardViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        dashboardViewModel =
                new ViewModelProvider(this).get(DashboardViewModel.class);
        View root = inflater.inflate(R.layout.fragment_dashboard, container, false);

        ((ConstraintLayout)root.findViewById(R.id.ProductContainer)).setOnClickListener((v -> {
            Intent view = new Intent(getContext(),  DetailProductActivity.class);
            view.setAction(Intent.ACTION_VIEW);
            startActivity(view);
        }));

        final ListView listView = root.findViewById(R.id.product_list_container);

        //listView.setAdapter();
        return root;
    }
}